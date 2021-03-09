import axios, {AxiosRequestConfig} from 'axios';
import {IStudents} from '../Pages/Students/types';

const api = axios.create({
    baseURL: process.env.REACT_APP_API_URL,
});

export function createStudent(student: IStudents, token: AxiosRequestConfig){
    return api.post('api/v1/students', student, token);
}

export function fetchStudents(page: number, token: AxiosRequestConfig, name: string){
    return api.get(`api/v1/students/asc/10/${page}/?name=${name}`, token);
}

export function findByIdStudent(id: number, token: AxiosRequestConfig){
    return api.get(`api/v1/students/${id}`, token);
}

export function updateStudent(student: IStudents, id: number, token: AxiosRequestConfig){
    return api.put(`api/v1/students/${id}`, student, token);
}

export function deleteStudent(id: number){
    return api.delete(`api/v1/students/${id}`);
}

export function renewCardStudent(id: number, student: IStudents, token: AxiosRequestConfig){
    return api.patch(`api/v1/students/renew/${id}`, student, token);
}