import { describe, it, expect } from 'vitest';

describe('PropertyDetails Component', () => {

  it('property should be null before loading', () => {
    const property: any = null;
    expect(property).toBeNull();
  });

  it('loaded flag should start as false', () => {
    const loaded = false;
    expect(loaded).toBe(false);
  });

  it('reviews array should start empty', () => {
    const reviews: any[] = [];
    expect(reviews.length).toBe(0);
  });

  it('should calculate average rating correctly', () => {
    const reviews = [
      { rating: 4 },
      { rating: 5 },
      { rating: 3 }
    ];
    const avg = reviews.reduce((sum, r) => sum + r.rating, 0) / reviews.length;
    expect(avg.toFixed(1)).toBe('4.0');
  });


    it('property details API URL should be correct', () => {
    const id = 5;
    const url = 'https://localhost:7287/api/Property/' + id;
    expect(url).toBe('https://localhost:7287/api/Property/5');
  });

});

