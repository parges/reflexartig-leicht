export class Testung
{
    id?: number;
    name: string;
    date: Date | string;
    chapters: TestungChapter[];
    patientId: number | null;
}

export interface TestungQuestion
{
    id: number | null;
    type: string;
    value: string;
    label: string;
    chapterId: number | null;
}


export interface TestungChapter
{
    id: number | null;
    name: string;
    score: number | null;
    questions: TestungQuestion[];
    testungId: number | null;

}
