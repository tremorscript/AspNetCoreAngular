import { Component, OnInit } from '@angular/core';
import { IProductDto, ProductService } from './product.service';
import { GridColumn, GridFieldType } from '@app/shared';

@Component({
  selector: 'appc-products',
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.scss'],
})
export class ProductsComponent implements OnInit {
  constructor(private productsClient: ProductService) {}

  products: IProductDto[];

  columns: GridColumn[] = [
    {
      field: 'productId',
      type: GridFieldType.Number,
      filter: true,
      sortable: true,
      width: 100,
    },
    {
      field: 'productName',
      filter: true,
      sortable: true,
      width: 180,
    },
    {
      field: 'unitPrice',
      type: GridFieldType.Number,
      filter: true,
      sortable: true,
      width: 120,
    },
    {
      field: 'supplierCompanyName',
      filter: true,
      sortable: true,
    },
    {
      field: 'categoryName',
      filter: true,
      sortable: true,
      width: 160,
    },
    {
      field: 'discontinued',
      type: GridFieldType.Boolean,
      filter: true,
      sortable: true,
      width: 120,
    },
    {
      type: GridFieldType.ActionButtons,
      cellRendererParams: {
        primaryClicked: this.editProduct.bind(this),
        secondaryClicked: this.deleteProduct.bind(this),
        primaryLabel: 'Edit Product',
        secondaryLabel: 'Delete Product',
      },
    },
  ];
  ngOnInit(): void {
    this.getData();
  }

  getData() {
    this.productsClient.getAll().subscribe((res) => {
      this.products = res.products;
    });
  }

  editProduct(product: IProductDto) {
    console.log(product);
    this.productsClient.update(product).subscribe(this.getData);
  }

  deleteProduct(product: IProductDto) {
    console.log(product);
    this.productsClient.delete(product.productId).subscribe(this.getData);
  }
}
