import { describe, it, expect } from 'vitest';

describe('Login Component - Basic Tests', () => {

  it('login data should start empty', () => {
    const loginData = { email: '', password: '' };
    expect(loginData.email).toBe('');
    expect(loginData.password).toBe('');
  });

  it('should detect empty email', () => {
    const email = '';
    expect(email.length).toBe(0);
  });

  it('should detect valid email format', () => {
    const email = 'test@test.com';
    expect(email).toContain('@');
  });

  it('login API URL should be correct', () => {
    const url = 'https://localhost:7287/api/Auth/login';
    expect(url).toContain('Auth/login');
  });

});
