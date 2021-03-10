import { IStudents } from "./types"

type props = {
    cardStudent: IStudents;
}

export default function StudentCard({cardStudent} : props){
    var date =  new Date(cardStudent.validate);
    return (
    <>
    <div className="students-content">
        
        <div className="students-photo"></div>
        <div className="students-input">
            <label>Nome: </label>
            <input className="students-name" value={cardStudent.name} disabled/>
            <label className="students-title-course">Curso: </label>
            <input className="students-course" value={cardStudent.course} disabled />
        </div>
        <div className="students-input-info"> 

            <label className="students-validate" >{date.toLocaleDateString()}</label> 
            <label>Código do Sistema: </label>
            <input className="students-registration" value={cardStudent.registrationNumber}  disabled/>
        </div>
    </div>
    <div className="students-content">
        
        <div className="students-photo"></div>
        <div className="students-input">
            <label>Nome: </label>
            <input className="students-name" value={cardStudent.name} disabled/>
            <label className="students-title-course">Curso: </label>
            <input className="students-course" value={cardStudent.course} disabled />
        </div>
        <div className="students-input-info"> 

            <label className="students-validate" >{date.toLocaleDateString()}</label> 
            <label>Código do Sistema: </label>
            <input className="students-registration" value={cardStudent.registrationNumber}  disabled/>
        </div>
    </div>
    </>

    )
}