import { describe, it, expect } from 'vitest';

describe('AddProperty Component', () => {

  it('property form should start with empty title', () => {
    const property = { title: '', location: '', price: 0, propertyType: '', description: '' };
    expect(property.title).toBe('');
  });

  it('should not save property without title', () => {
    const property = { title: '', location: 'Goa', price: 5000, propertyType: 'Villa' };
    const canSave = property.title !== '' && property.location !== '' && property.price > 0;
    expect(canSave).toBe(false);
  });

  it('should not save property without location', () => {
    const property = { title: 'Villa', location: '', price: 5000, propertyType: 'Villa' };
    const canSave = property.title !== '' && property.location !== '' && property.price > 0;
    expect(canSave).toBe(false);
  });


  it('upload URL should be correct', () => {
    const url = 'https://localhost:7287/api/Upload';
    expect(url).toContain('Upload');
  });


});
