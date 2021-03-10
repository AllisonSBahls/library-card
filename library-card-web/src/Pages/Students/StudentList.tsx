import { TYPE } from "react-toastify/dist/utils"
import StudentCard from "./StudentCard"
import { IStudents } from "./types"

type props = {
    Students: IStudents[];
}

export default function StudentList({Students}: props){
    return (
        <>
            {Students.map((student) => 
                <StudentCard
                    key={student.id}   
                    cardStudent = {student}
                />
            )}
        </>
    )
}