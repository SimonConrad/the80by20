import { InMemoryDbService } from 'angular-in-memory-web-api';

import { UserProblemData } from './data'
import { UserProblem } from '../solution-to-problem/model/UserProblem'
import { ProblemCategory } from '../solution-to-problem/model/ProblemCategory'


export class AppData implements InMemoryDbService {

  createDb(): { userProblems: UserProblem[], problemCategories: ProblemCategory[]} {
    const userProblems = UserProblemData.usersProblems;
    const problemCategories = UserProblemData.problemCategories;
    return { userProblems, problemCategories };
  }
}
