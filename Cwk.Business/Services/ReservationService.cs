using AutoMapper;
using Cwk.Business.Interfaces;
using Cwk.Domain.DTOs.Request;
using Cwk.Domain.DTOs.Response;
using Cwk.Domain.Entities;
using Cwk.Domain.Enums;
using Cwk.Domain.Interfaces;

namespace Cwk.Business.Services;

public class ReservationService : IReservationService
{
    private readonly IReservationRepository _reservationRepository;
    private readonly ISpaceRepository _spaceRepository;
    private readonly IMapper _mapper;

    public ReservationService(IReservationRepository reservationRepository, IMapper mapper, ISpaceRepository spaceRepository)
    {
        _reservationRepository = reservationRepository;
        _mapper = mapper;
        _spaceRepository = spaceRepository;
    }

    public async Task<bool> CancelReservationAsync(int reservationId)
    {
        var reservation = await _reservationRepository.GetByIdAsync(reservationId)
            ?? throw new ArgumentException("Reserva no encontrada");

        if (reservation.ReservationStatus == ReservationStatus.Cancelled)
            return false;

        reservation.ReservationStatus = ReservationStatus.Cancelled;
        await _reservationRepository.UpdateAsync(reservation);
        return true;
    }

    public async Task<SpaceAvailabilityDto?> CheckSpaceAvailabilityAsync(int spaceId, DateTime startTime, DateTime endTime)
    {
        var existingReservations = await _reservationRepository.GetAvailablesAsync(spaceId, startTime, endTime);

        var isAvailable = existingReservations.Count == 0;

        return new SpaceAvailabilityDto
        {
            SpaceId = spaceId,
            IsAvailable = isAvailable,
            ExistingReservations = _mapper.Map<List<ReservationDetailsDto>>(existingReservations)
        };
    }

    public async Task<bool> ConfirmReservationAsync(int reservationId)
    {
        var reservation = await _reservationRepository.GetByIdAsync(reservationId)
            ?? throw new ArgumentException("Reserva no encontrada");

        if (reservation.ReservationStatus != ReservationStatus.Pending)
            //  reservation.ReservationStatus == ReservationStatus.Pending
            return false;

        reservation.ReservationStatus = ReservationStatus.Confirmed;
        await _reservationRepository.UpdateAsync(reservation);
        return true;
    }

    public async Task<ReservationDetailsDto> CreateReservationAsync(CreateReservationRequestDto request)
    {
        var availability = await CheckSpaceAvailabilityAsync(request.SpaceId, request.StartTime, request.EndTime);
        if (!availability.IsAvailable)
        {
            throw new InvalidOperationException("El horario no esta disponible en el horario solicitado");
        }

        var space = await _spaceRepository.GetSpaceByIdAsync(request.SpaceId) ??
            throw new ArgumentException("Espacio no encontrado");

        // Calcular horas y monto total
        var hours = (int)Math.Ceiling((request.EndTime - request.StartTime).TotalHours);
        var totalAmount = space.PricePerHour * hours;

        var reservation = new Reservation
        {
            SpaceId = request.SpaceId,
            StartTime = request.StartTime,
            EndTime = request.EndTime,
            QuantityHours = hours,
            UserId = request.UserId,
            TotalAmount = totalAmount,
            ReservationStatus = ReservationStatus.Pending
        };

        await _reservationRepository.AddAsync(reservation);
        return _mapper.Map<ReservationDetailsDto>(reservation);
    }

    public async Task<bool> DeleteReservationAsync(int id)
    {
        var reservation = await _reservationRepository.GetByIdAsync(id);
        if (reservation == null)
            return false;

        reservation.ReservationStatus = ReservationStatus.Cancelled;
        await _reservationRepository.UpdateAsync(reservation);

        return true;
    }

    public async Task<ReservationDetailsDto?> GetReservationByIdAsync(int id)
    {
        var reservation = await _reservationRepository.GetByIdAsync(id);
        return reservation == null ? null : _mapper.Map<ReservationDetailsDto>(reservation);
    }

    public async Task<ReservationResponseDto> GetReservationsAsync(ReservationQueryDto query)
    {
        var reservations = await _reservationRepository.GetAllAsync();

        if (query.SpaceId.HasValue)
            reservations = [.. reservations.Where(r => r.SpaceId == query.SpaceId.Value)];

        if (query.UserId.HasValue)
            reservations = [.. reservations.Where(r => r.UserId == query.UserId.Value)];

        if (query.StartDate.HasValue)
            reservations = [.. reservations.Where(r => r.StartTime >= query.StartDate.Value)];

        if (query.EndDate.HasValue)
            reservations = [.. reservations.Where(r => r.EndTime <= query.EndDate.Value)];

        if (query.Status.HasValue)
            reservations = [.. reservations.Where(r => r.ReservationStatus == query.Status.Value)];

        var reservationDtos = _mapper.Map<List<ReservationDetailsDto>>(reservations);

        return new ReservationResponseDto
        {
            Reservations = reservationDtos,
            TotalCount = reservationDtos.Count,
            Success = true,
            Message = "Reservas obtenidas exitosamente"
        };
    }


    public async Task<ReservationDetailsDto?> UpdateReservationAsync(UpdateReservationRequestDto request)
    {
        var reservation = await _reservationRepository.GetByIdAsync(request.Id) ??
            throw new ArgumentException("Reserva no encontrada");

        if (reservation.ReservationStatus == ReservationStatus.Cancelled)
            throw new InvalidOperationException("No se puede actualizar una reserva cancelada");

        reservation.StartTime = request.StartTime;
        reservation.EndTime = request.EndTime;
        reservation.ReservationStatus = request.Status;

        var hours = (int)Math.Ceiling((request.EndTime - request.StartTime).TotalHours);
        
        var space = await _spaceRepository.GetSpaceByIdAsync(reservation.SpaceId) ?? 
            throw new ArgumentException("Espacio no encontrado");

        reservation.QuantityHours = hours;
        reservation.TotalAmount = space.PricePerHour * hours;

        await _reservationRepository.UpdateAsync(reservation);
        return _mapper.Map<ReservationDetailsDto>(reservation);
    }
}
