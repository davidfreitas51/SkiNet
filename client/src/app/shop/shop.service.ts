import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { IProduct } from '../shared/models/product';

@Injectable({
  providedIn: 'root'
})
export class ShopService {
  baseUrl = 'https://localhost:7016/api/'

  constructor(private http: HttpClient) { } 

  getProducts(){
    return this.http.get<IProduct[]>(this.baseUrl + 'products?sort=nameAsc&itemsPerPage=50&pageNumber=1');
  }
}
