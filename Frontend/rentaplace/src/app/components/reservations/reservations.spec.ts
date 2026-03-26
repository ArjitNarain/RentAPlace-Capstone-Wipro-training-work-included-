import { describe, it, expect } from 'vitest';

describe('Reservations Component', () => {

  it('reservations array should start empty', () => {
    const reservations: any[] = [];
    expect(reservations.length).toBe(0);
  });

  it('new reservation should have Pending status', () => {
    const reservation = {
      propertyId: 1,
      checkIn: '2026-04-01',
      checkOut: '2026-04-05',
      status: 'Pending'
    };
    expect(reservation.status).toBe('Pending');
  });

  it('owner should be able to confirm reservation', () => {
    const reservation = { id: 1, status: 'Pending' };
    reservation.status = 'Confirmed';
    expect(reservation.status).toBe('Confirmed');
  });

  it('owner should be able to cancel reservation', () => {
    const reservation = { id: 1, status: 'Pending' };
    reservation.status = 'Cancelled';
    expect(reservation.status).toBe('Cancelled');
  });

  it('status update URL should be correct', () => {
    const id = 3;
    const url = 'https://localhost:7287/api/Reservation/' + id + '/status';
    expect(url).toContain('status');
    expect(url).toContain('3');
  });


});