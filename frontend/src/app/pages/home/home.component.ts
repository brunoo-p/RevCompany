import { Component } from '@angular/core';
import { CardsComponent } from "../../components/home/cards/cards.component";
import { OrdersComponent } from "../../components/home/orders/orders.component";

@Component({
  selector: 'app-home',
  imports: [CardsComponent, OrdersComponent],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent {
}
