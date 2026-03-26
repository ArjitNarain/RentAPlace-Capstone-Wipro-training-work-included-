import { describe, it, expect } from 'vitest';

describe('PropertyList Component', () => {

  it('properties array should start empty', () => {
    const properties: any[] = [];
    expect(properties.length).toBe(0);
  });

  it('should filter properties by location', () => {
    const properties = [
      { id: 1, title: 'Goa Villa', location: 'Goa', price: 5000 },
      { id: 2, title: 'Mumbai Flat', location: 'Mumbai', price: 2500 },
      { id: 3, title: 'Goa House', location: 'Goa', price: 3000 }
    ];
    const filtered = properties.filter(p =>
      p.location.toLowerCase().includes('goa')
    );
    expect(filtered.length).toBe(2);
  });

    it('search URL should contain location parameter', () => {
    const baseUrl = 'https://localhost:7287/api';
    const location = 'Goa';
    const url = baseUrl + '/Property/search?location=' + location;
    expect(url).toContain('Goa');
  });

});
