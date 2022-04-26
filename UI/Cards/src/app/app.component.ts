import { Component, OnInit } from '@angular/core';
import { Card } from './models/card.model';
import { CardsService } from './service/cards.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
})
export class AppComponent implements OnInit {
  title = 'Cards';
  cards: Card[] = [];
  card: Card = {
    id: '',
    carddholderName: '',
    cardNumber: '',
    expiryMonth: '',
    expiryYear: '',
    cvc: '',
  };

  constructor(private cardService: CardsService) {}
  ngOnInit(): void {
    this.GetAllCards();
  }

  GetAllCards() {
    this.cardService.getAllCards().subscribe((response) => {
      this.cards = response;
    });
  }
  //add cards
  onSubmit() {
    if (this.card.id === '') {
      this.cardService.addCard(this.card).subscribe((response) => {
        this.GetAllCards();
        this.card = {
          id: '',
          carddholderName: '',
          cardNumber: '',
          expiryMonth: '',
          expiryYear: '',
          cvc: '',
        };
      });
    }
    else{
      this.updateCard(this.card)
    }
  }

  //delete card
  deleteCard(id: string) {
    this.cardService.deleteCard(id).subscribe((response) => {
      this.GetAllCards();
    });
  }
  // update card
  populateFrom(card: Card) {
    this.card = card;
    console.log(this.card);
    console.log(card);
  }

  updateCard(card: Card) {
    this.cardService.updateCard(card).subscribe(
      data => {
        this.GetAllCards();
      }
    )
  }
  Delete(card: Card) {
    this.card = {
      id: '',
      carddholderName: '',
      cardNumber: '',
      expiryMonth: '',
      expiryYear: '',
      cvc: '',
    };
  }
}
