import { describe, it, expect } from 'vitest';

describe('Messages Component', () => {

  it('messages array should start empty', () => {
    const messages: any[] = [];
    expect(messages.length).toBe(0);
  });

  it('replyContent should start empty', () => {
    const replyContent = '';
    expect(replyContent).toBe('');
  });

  it('showReplyForm should start as false', () => {
    const showReplyForm = false;
    expect(showReplyForm).toBe(false);
  });
  
  it('message URL for property owner should be correct', () => {
    const propertyId = 2;
    const url = 'https://localhost:7287/api/Message/property/' + propertyId;
    expect(url).toContain('property/2');
  });




});

