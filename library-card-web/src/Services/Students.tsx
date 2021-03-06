import axios, {AxiosRequestConfig} from 'axios';
import {IPhoto, IStudents} from '../Pages/Students/types';

const api = axios.create({
    baseURL: process.env.REACT_APP_API_URL,
});

export function createStudent(student: FormData, token: AxiosRequestConfig){
    return api.post('api/v1/students', student, token);
}

export function fetchStudents(page: number, token: AxiosRequestConfig, name: string, generateStatus: Boolean){
    return api.get(`api/v1/students/asc/10/${page}/?name=${name}&generate=${generateStatus}`, token);
}

export function findByIdStudent(id: number, token: AxiosRequestConfig){
    return api.get(`api/v1/students/${id}`, token);
}

export function updateStudent(student: FormData, id: number, token: AxiosRequestConfig){
    return api.put(`api/v1/students/${id}`, student, token);
}

export function deleteStudent(id: number, token: AxiosRequestConfig){
    return api.delete(`api/v1/students/${id}`);
}

export function renewCardStudent(id: number, student: IStudents, token: AxiosRequestConfig){
    return api.patch(`api/v1/students/renew/${id}`, student, token);
}