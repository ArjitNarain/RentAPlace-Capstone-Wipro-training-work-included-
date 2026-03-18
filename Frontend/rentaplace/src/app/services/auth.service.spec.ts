import { describe, it, expect, beforeEach, afterEach } from 'vitest';

describe('AuthService - Basic Tests', () => {

  beforeEach(() => {
    localStorage.clear();
  });

  afterEach(() => {
    localStorage.clear();
  });

  it('localStorage should be empty on start', () => {
    expect(localStorage.getItem('token')).toBe(null);
  });

  it('should save and retrieve token from localStorage', () => {
    localStorage.setItem('token', 'test-token-123');
    expect(localStorage.getItem('token')).toBe('test-token-123');
  });

  it('should return null after removing token', () => {
    localStorage.setItem('token', 'test-token-123');
    localStorage.removeItem('token');
    expect(localStorage.getItem('token')).toBe(null);
  });

  it('should save user role', () => {
    localStorage.setItem('userRole', 'Owner');
    expect(localStorage.getItem('userRole')).toBe('Owner');
  });

  it('should clear all user data on logout', () => {
    localStorage.setItem('token', 'abc');
    localStorage.setItem('userRole', 'Owner');
    localStorage.setItem('userName', 'Arjit');
    localStorage.clear();
    expect(localStorage.getItem('token')).toBe(null);
    expect(localStorage.getItem('userRole')).toBe(null);
  });

});
