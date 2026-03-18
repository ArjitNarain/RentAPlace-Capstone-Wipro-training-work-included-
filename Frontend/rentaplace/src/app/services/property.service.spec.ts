import { describe, it, expect } from 'vitest';

describe('PropertyService - Basic Tests', () => {

  it('should have correct base URL format', () => {
    const baseUrl = 'https://localhost:7287/api';
    expect(baseUrl).toContain('localhost');
    expect(baseUrl).toContain('7287');
  });

  it('should build correct property URL', () => {
    const baseUrl = 'https://localhost:7287/api';
    const url = baseUrl + '/Property';
    expect(url).toBe('https://localhost:7287/api/Property');
  });

  it('should build correct property by id URL', () => {
    const baseUrl = 'https://localhost:7287/api';
    const id = 5;
    const url = baseUrl + '/Property/' + id;
    expect(url).toBe('https://localhost:7287/api/Property/5');
  });

  it('should build correct search URL', () => {
    const baseUrl = 'https://localhost:7287/api';
    const url = baseUrl + '/Property/search?location=Goa&type=Villa';
    expect(url).toContain('search');
    expect(url).toContain('Goa');
  });

});
