import { Component } from '@angular/core';
import {BookService} from "./services/book.service";
import {ICalculationBookQuery} from "./Model/penalty-bussiness-model";

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  public checkIn: Date;
  public checkOut: Date;
  public countryId: any;
  public bookCalculation: any;

  constructor(private bookService: BookService) {
  }


  public calculate(): void  {
    const model: ICalculationBookQuery = {
      countryId: this.countryId,
      checkedInDate: this.checkIn,
      checkedOutDate: this.checkOut
    };

    this.bookService.getCalculationBook(model).toPromise().then((result) => {
      this.bookCalculation = result;
    }).catch((e)=> {
      console.log(e);
    });
  }
}
