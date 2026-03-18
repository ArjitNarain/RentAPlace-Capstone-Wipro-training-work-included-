import { Component, OnInit } from '@angular/core';
import { MessageService, Message } from '../../services/message.service';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-messages',
  standalone: false,
  templateUrl: './messages.html'
})
export class Messages implements OnInit {

  messages: Message[] = [];
  newMessage: string = '';
  receiverId: number = 0;

  constructor(
    private messageService: MessageService,
    public authService: AuthService
  ) {}

  ngOnInit(): void {
    this.loadMessages();
  }

  loadMessages() {
    this.messageService.getMessages().subscribe((data: Message[]) => {
      this.messages = data;
    });
  }

  // Compare as numbers to avoid type mismatch error
  isMine(userId: number | undefined): boolean {
    return Number(this.authService.getUserId()) === userId;
  }

  sendMessage() {
    if (!this.newMessage.trim()) {
      alert('Please type a message.');
      return;
    }
    if (!this.receiverId || this.receiverId <= 0) {
      alert('Please enter a valid receiver ID.');
      return;
    }

    const message: Message = {
      content: this.newMessage,
      receiverId: this.receiverId
    };

    this.messageService.sendMessage(message).subscribe((res: any) => {
      this.newMessage = '';
      this.loadMessages();
    }, err => {
      alert('Failed to send. Please login first.');
    });
  }

}
