using Cwk.Domain.Entities;
using Cwk.Domain.Enums;
using Cwk.Domain.Interfaces;
using Cwk.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Cwk.Infrastructure.Repositories;

public class ReservationRepository : IReservationRepository
{
    private readonly AppDbContext _context;

    public ReservationRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Reservation> AddAsync(Reservation reservation)
    {
        await _context.Reservations.AddAsync(reservation);
        await _context.SaveChangesAsync();
        return reservation;
    }

    public async Task<List<Reservation>> GetAllAsync()
    {
        return await _context.Reservations
            .AsNoTracking()
            .Include(r => r.User)
            .Include(r => r.Space)
            .ToListAsync();
    }

    public async Task<List<Reservation>> GetAvailablesAsync(int spaceId, DateTime startTime, DateTime endTime)
    {
        var existingReservations = await _context.Reservations
            .AsNoTracking()
            .Where(r => r.SpaceId == spaceId &&
                        r.ReservationStatus != ReservationStatus.Cancelled &&
                        ((r.StartTime <= startTime && r.EndTime > startTime) ||
                         (r.StartTime < endTime && r.EndTime >= endTime) ||
                         (r.StartTime >= startTime && r.EndTime <= endTime)))
            .Include(r => r.User)
            .ToListAsync();

        return existingReservations;
    }

    public async Task<Reservation?> GetByIdAsync(int id)
    {
        var reservation = await _context.Reservations
            .FindAsync(id);
        return reservation;
    }

    public async Task<List<Reservation>> GetBySpaceIdAsync(int spaceId)
    {
        var reservations = await _context.Reservations
            .Where(r => r.SpaceId == spaceId)
            .ToListAsync();

        return reservations;
    }

    public async Task<Reservation> UpdateAsync(Reservation reservation)
    {
        _context.Update(reservation);
        await _context.SaveChangesAsync();
        return reservation;
    }
}
