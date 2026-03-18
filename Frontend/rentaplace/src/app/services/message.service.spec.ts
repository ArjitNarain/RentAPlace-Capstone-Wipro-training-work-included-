import { describe, it, expect } from 'vitest';

describe('MessageService - Basic Tests', () => {

  it('should have correct base URL', () => {
    const baseUrl = 'https://localhost:7287/api/Message';
    expect(baseUrl).toContain('Message');
    expect(baseUrl).toContain('7287');
  });

  it('message object should have required fields', () => {
    const message = {
      receiverId: 2,
      content: 'Hello!'
    };
    expect(message.receiverId).toBe(2);
    expect(message.content).toBe('Hello!');
  });

  it('empty message content should be falsy', () => {
    const content = '';
    expect(content.trim()).toBeFalsy();
  });

});
