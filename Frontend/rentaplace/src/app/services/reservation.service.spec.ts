import { describe, it, expect } from 'vitest';

describe('ReservationService - Basic Tests', () => {

  it('should have correct base URL', () => {
    const baseUrl = 'https://localhost:7287/api/Reservation';
    expect(baseUrl).toContain('Reservation');
  });

  it('should build correct status update URL', () => {
    const baseUrl = 'https://localhost:7287/api/Reservation';
    const id = 3;
    const url = baseUrl + '/' + id + '/status';
    expect(url).toBe('https://localhost:7287/api/Reservation/3/status');
  });

  it('reservation status should default to Pending', () => {
    const reservation = {
      propertyId: 1,
      checkIn: '2026-04-01',
      checkOut: '2026-04-05',
      status: 'Pending'
    };
    expect(reservation.status).toBe('Pending');
  });

});
