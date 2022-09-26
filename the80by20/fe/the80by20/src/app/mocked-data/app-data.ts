import { InMemoryDbService } from 'angular-in-memory-web-api';

import { UserProblemData } from './data'
import { UserProblem } from '../solution-to-problem/model/UserProblem'


export class AppData implements InMemoryDbService {

  createDb(): { userProblems: UserProblem[]} {
    const userProblems = UserProblemData.usersProblems;
    return { userProblems };
  }
}
