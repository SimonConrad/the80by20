export interface OperatorSolutionToProblem{
    id: string;
    userId: string;
    requiredSolutionTypes: string;
    description?: string;
    category?: string;
    isConfirmed?: boolean;
    isRejected: boolean;
    solutionToid?: string;
    price?: number;
    solutionSummary?: string;
    solutionElements?: string;
    workingOnSolutionEnded: boolean;
    createdAt: string;
}
