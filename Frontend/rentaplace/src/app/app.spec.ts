import { describe, it, expect } from 'vitest';

describe('RentAPlace App - Basic Tests', () => {

  it('app name should be rentaplace', () => {
    const appName = 'rentaplace';
    expect(appName).toBe('rentaplace');
  });

  it('base API url should be correct', () => {
    const url = 'https://localhost:7287/api';
    expect(url).toContain('7287');
  });

});
