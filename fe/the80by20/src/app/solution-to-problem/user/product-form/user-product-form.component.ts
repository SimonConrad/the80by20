import { ChangeDetectionStrategy, Component, Input, OnInit } from '@angular/core';
import { catchError, EMPTY, Observable, Subject, tap } from 'rxjs';
import { UserProblem } from '../model/UserProblem';
import { UserProblemService } from '../user-problem.service';
import { UUID } from 'angular2-uuid';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';


@Component({
  selector: 'app-user-product-form',
  templateUrl: './user-product-form.component.html',
  styleUrls: ['./user-product-form.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush //// INFO ChangeDetectionStrategy.OnPush only detect changes made from:
  // - input properties, and events from child components (output),
  // - observables bound in the template using an async pipe;
  // bound values set in local properties won't trigger change detection, so won't update the ui
})
export class UserProductFormComponent implements OnInit {
  title = 'User Problem From';

  //@Input()
  problem: UserProblem = {
    id: "",
    userId: "",
    requiredSolutionTypes: "",
    description: "",
    categoryId: "",
    category: "",
    isConfirmed: false,
    isRejected: false,
    createdAt: "",
    color: ""
  };

  private errorMessageSubject = new Subject<string>()
  errorMessageDataStream$ = this.errorMessageSubject.asObservable();

  // INFO problem observable data item stream, which emits Problem or undefined
  // INFO  user-problem-component already subsribed to this observable using async pipe (in its html temaplte)
  selectProblemActionStream$ = this.problemService.selectProblemActionStream$
    .pipe(
      tap(selectedProblem => {
        this.problem = { ...selectedProblem! };
        this.description.setValue(this.problem.description);
      }),
      catchError(err => {
        //this.errorMessage = err; //INFO when changeDetection: ChangeDetectionStrategy.OnPush then this won't trigger chnage detection and error will not appear to the user
        this.errorMessageSubject.next(err); // emit value to the stream
        return EMPTY;
      })
    );

  updateProblemActionStream$ = this.problemService.updateProblemActionStream$.pipe(
    catchError(err => {
      this.errorMessageSubject.next(err); // INFO emit value to the stream
      return EMPTY;
    }),
    tap(() => this.problemService.startSelectAction(undefined)),
  );

  addProblemActionStream$ = this.problemService.addProblemActionStream$.pipe(
    catchError(err => {
      this.errorMessageSubject.next(err); // emit value to the stream
      return EMPTY;
    }),
    tap(() => this.problemService.startSelectAction(undefined))
  );

  description = new FormControl('', []);

  problemForm: FormGroup = this.fb.group({
    description: this.description,
  });

  constructor(private problemService: UserProblemService, private fb: FormBuilder,) { }
  
  ngOnInit(): void {
    
  }

  onSave(): void {
    //INFO alternative (not fully reactive way) is to
    // call update service and subscribe to it (invoke it thi way),
    // but rember then about the need to unsubcribe it in ondestroy
    // use Subscription class

    if (this.problem.id === '') {
      this.problem.id = UUID.UUID();
      this.problem.description = this.description.value!;
      this.problemService.startAdd(this.problem);

    } else {
      this.problem.description = this.description.value!;
      this.problemService.startUpdate(this.problem);
    }
  }

  onCancel(): void {
    this.problemService.startSelectAction(undefined);
  }
}
