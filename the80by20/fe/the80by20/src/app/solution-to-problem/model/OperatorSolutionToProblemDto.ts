export interface OperatorSolutionToProblemDto{
    problemId: string;
    userId: string;
    requiredSolutionTypes: string;
    description?: string;
    category?: string;
    isConfirmed?: boolean;
    isRejected: boolean;
    solutionToProblemId?: string;
    price?: number;
    solutionSummary?: string;
    solutionElements?: string;
    workingOnSolutionEnded: boolean;
    createdAt: string;
}
