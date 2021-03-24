import { IStudents } from "./types";
import "./style.css";
import { useEffect, useState } from "react";
import { fetchStudents } from "../../Services/Students";
import { toast } from "react-toastify";
import StudentList from "./StudentList";
import Navbar from "../Navbar";
import StudentForm from "./StudentForm";

export default function Students() {
  const [students, setStudents] = useState<IStudents[]>([]);
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
    fetchAllStudents();
  }, [token, nameStudent, refresh]);

  async function fetchAllStudents() {
    try {
      const response = await fetchStudents(page, authorization, nameStudent);
      setTotalResult(response.data.totalResultS);
      setStudents(response.data.list);
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
        {/* <div>
          <div className="students-header">
            <h3>Bem vindo ao Library Card Allison</h3>
            <div className="students-header-button">
              <button className="students-button-newcard">
                Gerar Carteirinha
              </button>
              <button className="students-button-renewcard">
                Renovar Carteirinhas
              </button>
              <button className="students-button-expirate">
                Próximas a Vencer
              </button>
            </div>
          </div>
        </div> */}

        <div className="students">
        <div className="students-form">
            <StudentForm />
          </div>
          <div className="students-list-cards">
            <StudentList Students={students} />
          </div>
        
        </div>
      </div>
    </>
  );
}
