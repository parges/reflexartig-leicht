export class Anamnese
{
    id?: number;
    name: string;
    date: Date | string;
    countOfPositivAnswers?: number;
    chapters: AnamneseChapter[];
    patientId: number | null;
}

export interface AnamneseQuestion
{
    id: number | null;
    type: string;
    value: string;
    label: string;
    metaInfo: string;
    textPrefix: string;
    textValue: string;
    chapterId: number | null;
}


export interface AnamneseChapter
{
    id: number | null;
    name: string;
    questions: AnamneseQuestion[];
    anamneseId: number | null;

}
