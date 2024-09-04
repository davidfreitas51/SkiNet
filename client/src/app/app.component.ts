import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  title = 'SkiNet';

  constructor(private http: HttpClient){

  }
  ngOnInit(): void {
    this.http.get('https://localhost:7016/api/products').subscribe((response: any) => {
      console.log(response)
    }, error =>{
      console.log(error)
    });
  }
}
