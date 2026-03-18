import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface Message {
  id?: number;
  senderId?: number;
  receiverId?: number;
  content: string;
  sentAt?: string;
}

@Injectable({
  providedIn: 'root'
})
export class MessageService {

  baseUrl = 'https://localhost:7287/api/Message';  // fixed port (was 5001)

  constructor(private http: HttpClient) {}

  getMessages(): Observable<Message[]> {
    return this.http.get<Message[]>(this.baseUrl);
  }

  sendMessage(message: any): Observable<any> {
    return this.http.post(this.baseUrl, message);
  }

}
