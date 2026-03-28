import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({ providedIn: 'root' })
export class UserService {
  private baseUrl = 'https://localhost:44375/gateway/api/user';
  private headers = new HttpHeaders({ 'x-api-key': 'my-secret-key' });

  constructor(private http: HttpClient) {}

  getUserById(id: string) {
    console.log('ID: ', id);
    return this.http.get<any>(`${this.baseUrl}/GetUserById/${id}`, {
      headers: this.headers
    });
  }

  getUsers() {
    return this.http.get<any[]>(`${this.baseUrl}/GetUsers`, {
      headers: this.headers
    });
  }
}