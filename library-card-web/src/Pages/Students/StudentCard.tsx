import { IStudents } from "./types"
import {BiEdit} from "react-icons/bi";
import {TiDelete} from "react-icons/ti"
type props = {
    cardStudent: IStudents;
    deleteCard: (id: number) => void;
}

export default function StudentCard({cardStudent, deleteCard} : props){
    var date =  new Date(cardStudent.validate);
    return (
    <>
        <div className="students-content">
            <div className="students-buttons">
                <button className="students-button-edit"><BiEdit/></button>
                <button onClick={() => 
                    {if (window.confirm(`Você tem certeza que deseja remover: ${cardStudent.name}`))
                    {deleteCard(cardStudent.id)}
                    }} className="students-button-delete"><TiDelete/></button>
            </div>
            <div className="students-photo">
                <img src={`https://localhost:5001/resources/images/${cardStudent.photo}`} width="70px" height="80px" alt="Foto do aluno"/>
                </div>
            <div className="students-input">
                <label>Nome: </label>
                <input className="students-name" value={cardStudent.name} disabled/>
                <label className="students-title-course">Curso: </label>
                <input className="students-course" value={cardStudent.course} disabled />
            </div>
            <div className="students-input-info"> 

                <label className="students-expire" >{date.toLocaleDateString()}</label> 
                <label>Código do Sistema: </label>
                <input className="students-registration" value={cardStudent.registrationNumber}  disabled/>
            </div>
        </div>
    </>

    )
}