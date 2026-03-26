
import { Component, OnInit, ChangeDetectorRef, NgZone } from '@angular/core';
import { MessageService, Message } from '../../services/message.service';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-messages',
  standalone: false,
  templateUrl: './messages.html'
})
export class Messages implements OnInit {

  messages: Message[] = [];

  // Reply variables
  replyContent: string = '';
  replyReceiverId: number = 0;
  replyingToName: string = '';
  showReplyForm: boolean = false;

  constructor(
    private messageService: MessageService,
    public authService: AuthService,
    private cdr: ChangeDetectorRef,
    private ngZone: NgZone
  ) {}

  ngOnInit(): void {
    this.loadMessages();
  }


  loadMessages() {
    this.messageService.getMessages().subscribe((data: Message[]) => {
      this.ngZone.run(() => {
        this.messages = data;
        this.cdr.detectChanges();
      });
    });
  }

  

  
  isMine(userId: number | undefined): boolean {
    return Number(this.authService.getUserId()) === userId;
  }
//entire message thing code
  // When reply button is clicked on a message
  setReply(message: Message) {
    // Reply goes to the OTHER person in the message
    if (this.isMine(message.senderId)) {
      // I sent this messagereply goes to receiver
      this.replyReceiverId = message.receiverId!;
      this.replyingToName = 'User #' + message.receiverId;
    } else {
      // Someone sent me this messagereply goes to sender
      this.replyReceiverId = message.senderId!;
      this.replyingToName = 'User #' + message.senderId;
    }
    this.showReplyForm = true;
    this.replyContent = '';
    this.cdr.detectChanges();
  }


  cancelReply() {
    this.ngZone.run(() => {
      this.showReplyForm = false;
      this.replyContent = '';
      this.replyReceiverId = 0;
      this.replyingToName = '';
      this.cdr.detectChanges();
    });
  }

  

  sendReply() {
    if (!this.replyContent.trim()) {
      alert('Please type a message.');
      return;
    }

    if (!this.replyReceiverId || this.replyReceiverId <= 0) {
      alert('Could not find receiver. Please click Reply button again.');
      return;
    }

    const message: Message = {
      content: this.replyContent,
      receiverId: this.replyReceiverId
    };


    this.messageService.sendMessage(message).subscribe(
      () => {
        this.ngZone.run(() => {
          this.replyContent = '';
          this.showReplyForm = false;
          this.replyReceiverId = 0;
          this.replyingToName = '';
          this.loadMessages();
        });
        alert('Reply sent!');
      },
      () => { alert('Failed to send. Please login first.'); }
    );
  }

}




  

    