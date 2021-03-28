export interface IStudents {
    id: number;
    name: string;
    course: string;
    registrationNumber: number
    photo: string;
    validate: Date;
    imageFile: File | null;

}
export interface IPhoto{
    imageFile:null;
    photo: string;
}