import StudentCard from "./StudentCard";
import { IStudents } from "./types";

type props = {
  StudentsGenerated: IStudents[];
  StudentsNotGenerated: IStudents[];
  deleteCard: (id: number) => void;
};

export default function StudentList({
  StudentsGenerated,
  StudentsNotGenerated,
  deleteCard
}: props) {
  return (
    <>
      <div className="generate-title">
        <h3 className="students-list-title">Carteirinhas para Gerar</h3>
        <button className="generate-button">Gerar Carteirinha</button>
      </div>
      <div className="students-list">
        {StudentsNotGenerated.map((student) => (
          <StudentCard 
            key={student.id} 
            cardStudent={student}
            deleteCard={deleteCard} />
        ))}
      </div>
      <h3 className="students-list-title"> Últimas carteirinhas criadas</h3>
      <div className="students-list">
        {StudentsGenerated.map((student) => (
          <StudentCard 
            key={student.id}
            cardStudent={student}
            deleteCard={deleteCard} />
        ))}
      </div>
    </>
  );
}
