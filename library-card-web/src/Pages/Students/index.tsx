import { IStudents } from "./types";
import "./style.css";
import { useEffect, useState } from "react";
import { fetchStudents } from "../../Services/Students";
import { toast } from "react-toastify";
import StudentList from "./StudentList";
import Navbar from "../Navbar";
import StudentForm from "./StudentForm";

export default function Students() {
  const [studentsGenerated, setStudents] = useState<IStudents[]>([]);
  const [studentsNotGenerated, setStudentsNotGenerated] = useState<IStudents[]>([]);
  const [totalResult, setTotalResult] = useState<number>(0);
  const [nameStudent, setNameStudent] = useState<string>("");
  const [isLoading, setIsLoading] = useState<Boolean>(false);
  const [page, setPage] = useState<number>(1);
  const [studentId, setStudentId] = useState<number>(0);
  const [refresh, setRefresh] = useState(0);

  const token = localStorage.getItem("Token");

  const authorization = {
    headers: {
      Authorization: `Bearer eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOlsiMSIsImluZm9ybWF0aWNhIl0sIm5iZiI6MTYxNjExNjQwOCwiZXhwIjoxNjE2MjAyODA4LCJpYXQiOjE2MTYxMTY0MDh9.DIfbjDxUSse7ZNDPlAO2X9WF6bdEPNgrE-gLQY1VuHpeQ1IOPYIO8-IiANrVttYZeDQ-HEi3ObH3lLzk_NC3Rw`,
    },
  };

  useEffect(() => {
    fetchStudentsGenerated();
    fetchStudentsNeedToGenerated();
  }, [token, nameStudent, refresh]);

  async function fetchStudentsGenerated() {
    try {
      const response = await fetchStudents(page, authorization, nameStudent, true);
      setTotalResult(response.data.totalResultS);
      setStudents(response.data.list);
      setPage(page);
      setIsLoading(true);
    } catch (err) {
      toast.error("Erro ao listar os estudantes");
    }
  }

  async function fetchStudentsNeedToGenerated() {
    try {
      const response = await fetchStudents(page, authorization, nameStudent, false);
      setTotalResult(response.data.totalResultS);
      setStudentsNotGenerated(response.data.list);
      setPage(page);
      setIsLoading(true);
    } catch (err) {
      toast.error("Erro ao listar os estudantes");
    }
  }

  return (
    <>
      <Navbar />
      <div className="students-container">

        <div className="students">
        <div className="students-form">
          <h3 className="students-form-title">Criar Carteirinha da Biblioteca</h3>
            <StudentForm />
          </div>


          <div className="students-list-cards">
            <h2>Carteirinhas</h2>
            <StudentList 
              StudentsGenerated={studentsGenerated}
              StudentsNotGenerated={studentsNotGenerated} />
          </div>
        
        </div>
      </div>
    </>
  );
}
