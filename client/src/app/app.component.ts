import { NgFor } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { Component, inject, OnInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, NgFor],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent implements OnInit{
  http = inject(HttpClient);
  title = 'DatingApp';
  users: any;


  ngOnInit(): void {
    this.http.get('http://localhost:5203/api/users').subscribe({
      next: response => this.users = response,
      error: err => console.log(err),
      complete: () => console.log('Ukończenie Żądanie.')
    })
    //throw new Error('Method not implemented.');
  }
}
