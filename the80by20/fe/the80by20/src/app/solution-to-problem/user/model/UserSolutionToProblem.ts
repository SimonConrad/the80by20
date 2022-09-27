export interface UserSolutionToProblem {
  id: string;
  userId: string;
  requiredSolutionTypes: string;
  description: string;
  category?: string;
  solutionToid?: string;
  price?: number;
  solutionSummary?: string;
  solutionElements?: string;
  workingOnSolutionEnded: boolean;
  createdAt: string;
}
