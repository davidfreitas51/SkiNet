import { Component, OnInit } from '@angular/core';
import { IProduct } from '../shared/models/product';
import { ShopService } from './shop.service';
import { IBrand } from '../shared/models/brands';
import { IType } from '../shared/models/productType';
import { ShopParams } from '../shared/models/shopParams';

@Component({
  selector: 'app-shop',
  templateUrl: './shop.component.html',
  styleUrls: ['./shop.component.scss']
})
export class ShopComponent implements OnInit {
  products: IProduct[]
  brands: IBrand[]
  types: IType[]
  shopParams = new ShopParams()
  sortOption = [
    { name: 'Alphabetical', value: 'name' },
    { name: 'Price: Low to high', value: 'priceAsc'},
    { name: 'Price: High to low', value: 'priceDesc'}
  ];

  constructor(private shopService: ShopService) { }

  ngOnInit(): void {
    this.getProducts();
    this.getBrands();
    this.getTypes();
  }

  getProducts(){
    this.shopService.getProducts(this.shopParams).subscribe(response =>{
      this.products = response;
    }, error => {
      console.log(error);
    });
  }

  getBrands(){
    this.shopService.getBrands().subscribe(response =>{
      this.brands = [{id: 0, name: 'All'}, ...response]
    }, error => {
      console.log(error)
    });
  }

  getTypes(){
    this.shopService.getTypes().subscribe(response =>{
      this.types = [{id: 0, name: 'All'}, ...response];
    }, error => {
      console.log(error)
    });
  }

  onBrandSelected(brandId: number){
    this.shopParams.brandId = brandId
    this.getProducts();
  }

  onTypeSelected(typeId: number){
    this.shopParams.typeId = typeId
    this.getProducts();
  }

  onSortSelected(sort: string){
    this.shopParams.sort = sort;
    this.getProducts();
  }
}
