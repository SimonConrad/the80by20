export interface UserSolutionToProblemDto {
  problemId: string;
  userId: string;
  requiredSolutionTypes: string;
  description?: string;
  category?: string;
  solutionToProblemId?: string;
  price?: number;
  solutionSummary?: string;
  solutionElements?: string;
  workingOnSolutionEnded: boolean;
  createdAt: string;
}
