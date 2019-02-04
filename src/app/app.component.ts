import { Component } from '@angular/core';

import { MatIconRegistry } from '@angular/material/icon';
import { DomSanitizer } from '@angular/platform-browser';
import { GithubService } from './Shared/Service/github.service';
import { Observable, observable, of } from 'rxjs';
import { FormControl } from '@angular/forms';

import {
  startWith,
  map,
  debounceTime,
  mergeMapTo,
  mergeMap,
  switchMap,
  catchError
} from 'rxjs/operators';
import { Items } from './interface';


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'ueltest';

  public githubAutoComplete$: Observable<Items> = null;
  public autoCompleteControl = new FormControl();

  constructor(
    private matIconRegistry: MatIconRegistry,
    private domSanitizer: DomSanitizer,
    private githubService: GithubService) {
    this.matIconRegistry.addSvgIcon(
      'airplane',
      this.domSanitizer.bypassSecurityTrustResourceUrl('../assets/airplane.svg')
    );
  }

  lookup(value: string): Observable<Items> {
    return this.githubService.search(value.toLowerCase()).pipe(
      // map the item property of the github results as our return object
      map(results => results.items),
      // catch errors
      catchError(_ => {
        return of(null);
      })
    );
  }

  ngOnInit() {
    this.githubAutoComplete$ = this.autoCompleteControl.valueChanges.pipe(
      startWith(''),
      // delay emits
      debounceTime(300),
      // use switch map so as to cancel previous subscribed events, before creating new once
      switchMap(value => {
        if (value !== '') {
          // lookup from github
          return this.lookup(value);
        } else {
          // if no value is pressent, return null
          return of(null);
        }
      })
    );
  }

}
