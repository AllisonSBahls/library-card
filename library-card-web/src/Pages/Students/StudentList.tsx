import StudentCard from "./StudentCard"
import { IStudents } from "./types"

type props = {
    Students: IStudents[];
}

export default function StudentList({Students}: props){
    return (
        <>
        <h3 className="students-list-title">Carteirinhas para Gerar</h3>
         <div className="students-list">
            {Students.map((student) => 
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