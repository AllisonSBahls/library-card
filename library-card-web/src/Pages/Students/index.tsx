import StudentCard from "./StudentCard";
import {IStudents} from './types';
import "./style.css";
import { useEffect, useState } from "react";
import { fetchStudents } from "../../Services/Students";
import { toast } from "react-toastify";

export default function Students() {
      
    const [student, setStudents] = useState<IStudents[]>([]);
    const [totalResult, setTotalResult] = useState<number>(0);
    const [nameStudent, setNameStudent] = useState<string>("");
    const [isLoading, setIsLoaind] = useState<Boolean>(false);
    const [page, setPage] = useState<number>(1);
    const [studentId, setStudentId] = useState<number>(0);
  const [refresh, setRefresh] = useState(0);

    const token = localStorage.getItem('Token');

    const authorization = { 
        headers: {
            Authorization: `Bearer eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOlsiMSIsImFsbGlzb24iXSwibmJmIjoxNjE1MzEwMTA3LCJleHAiOjE2MTUzOTY1MDcsImlhdCI6MTYxNTMxMDEwN30.0zACzbY4J-6BFNjHh3XrGGz24EwXYICy8FixNV-wRRpxRtG9qq1Z3knsnyUGbnBKPiKojQUi_TNwdzjzM8mwwQ`
        }
    }

    useEffect(() =>{
        fetchAllStudents();
    }, [token, nameStudent, refresh])

    async function fetchAllStudents(){
        try{
            const response = await fetchStudents(page, authorization, "a");
            console.log(response);
        }catch (err){
            toast.error("Erro ao listar os estudantes")
        }
    }


    return (
        <>
            <div className="students-container">

                {/* Cards */}
                <StudentCard/>
                <StudentCard/>
                <StudentCard/>
                <StudentCard/>

            </div>
        </>
    )
}