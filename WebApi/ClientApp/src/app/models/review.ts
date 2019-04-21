export class Review
{
    id?: number;
    name: string;
    date: string;

    /*
      * Review Overview
      */
    payed: boolean;
    exercises: string;
    reasons: string;
    /*
      * Review Details
      */
    observationsParents: string;
    observationsChild: string;
    exerciseAccomplishment: string;
    problemHierarchies: ProblemHierarchie[];
    chapters: ReviewChapter[];

    patientId?: number;

}

export interface ProblemHierarchie
{
    id: number | null;
    initialValue: string;
    changedValue: string;

    reviewId: number | null;

}

export interface ReviewChapter
{
    id: number | null;
    name: string;
    score: number | null;

    questions: ReviewQuestion[];

    reviewId: number | null;
}

export interface ReviewQuestion
{
    id: number | null;
    type: string;
    label: string;
    value: string;

    reviewChapterId: number | null;
}
