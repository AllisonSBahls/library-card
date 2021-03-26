import StudentCard from "./StudentCard"
import { IStudents } from "./types"

type props = {
    StudentsGenerated: IStudents[];
    StudentsNotGenerated: IStudents[];
}

export default function StudentList({StudentsGenerated: Students, StudentsNotGenerated: NotGenerated}: props){
    return (
        <>
        <h3 className="students-list-title">Carteirinhas para Gerar</h3>
         <div className="students-list">
            {NotGenerated.map((student) => 
                <StudentCard
                    key={student.id}   
                    cardStudent = {student}
                />
            )}
        </div>
        <h3 className="students-list-title"> Últimas carteirinhas criadas</h3>
            <div className="students-list">
            {Students.map((student) => 
                <StudentCard
                    key={student.id}   
                    cardStudent = {student}
                />
            )}
            </div>
        </>
    )
}