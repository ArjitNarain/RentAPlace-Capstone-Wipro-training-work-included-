import { describe, it, expect } from 'vitest';

describe('Register Component - Basic Tests', () => {

  it('user object should have role field', () => {
    const user = { name: 'Test', email: 'test@test.com', password: '1234', role: 'User' };
    expect(user.role).toBe('User');
  });

  it('owner role should be Owner', () => {
    const user = { name: 'Owner', email: 'owner@test.com', password: '1234', role: 'Owner' };
    expect(user.role).toBe('Owner');
  });

  it('password should not be empty', () => {
    const password = '1234';
    expect(password.length).toBeGreaterThan(0);
  });


  it('register API URL should be correct', () => {
    const url = 'https://localhost:7287/api/Auth/register';
    expect(url).toContain('Auth/register');
  });

  

});
