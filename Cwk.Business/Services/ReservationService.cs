using AutoMapper;
using Cwk.Business.Interfaces;
using Cwk.Domain.DTOs.Request;
using Cwk.Domain.DTOs.Response;
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

    public async  Task<bool> ConfirmReservationAsync(int reservationId)
    {
        var reservation = await _reservationRepository.GetByIdAsync(reservationId)
            ?? throw new ArgumentException("Reserva no encontrada");

        if(reservation.ReservationStatus != ReservationStatus.Pending)
          //  reservation.ReservationStatus == ReservationStatus.Pending
            return false;

        reservation.ReservationStatus = ReservationStatus.Confirmed;
        await _reservationRepository.UpdateAsync(reservation);
        return true;
    }

    public Task<ReservationDetailsDto> CreateReservationAsync(CreateReservationRequestDto request)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteReservationAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<ReservationDetailsDto?> GetReservationByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<ReservationResponseDto> GetReservationsAsync(ReservationQueryDto query)
    {
        throw new NotImplementedException();
    }

    public Task<ReservationDetailsDto?> UpdateReservationAsync(UpdateReservationRequestDto request)
    {
        throw new NotImplementedException();
    }
}
