import React, { useEffect, useState } from "react";
import { toast } from 'react-toastify'
import { createStudent } from "../../Services/Students";
import { IStudents } from "./types";

type Props ={
  student: IStudents;
}

export default function StudentForm({student}: Props) {
  const defaultPhoto = '/Image/default_photo.png'

  const ImageProps = {
    imageFile: null,
    imagePhoto: defaultPhoto
  }

  const [values, setValues] = useState<IStudents>(student);
  const [image, setImage] = useState(ImageProps);
  const [name, setName] = useState('');
  const [id, setId] = useState(0);
  const [course, setCourse]= useState('');
  const [expiration, setExpiration]= useState('');
  const [registrationNumber, setRegistrationNumber] = useState('');
  const token = localStorage.getItem("Token");

  const authorization = {
    headers: {
      Authorization: `Bearer token`,
    },
  };


  useEffect(() =>{
    if(student.id === 0) formCardDefault();
    else loadCardStudent();
  }, [student])

  const showPreview = (e: any) => {
    if (e.target.files && e.target.files[0]){
      let imageFile = e.target.files[0];
      const reader = new FileReader();
      reader.onload = (x: any) => {
        setImage({
          imageFile,
          imagePhoto: x.target.result
        });
      }
      reader.readAsDataURL(imageFile); 

    }
    else{
      setImage({
        imageFile: null,
        imagePhoto: defaultPhoto
      });
    }
  }

  async function formCardDefault(){
    setName('');
    setId(0);
    setCourse('');
    setRegistrationNumber('');
    setImage({
      imageFile: null,
      imagePhoto: defaultPhoto
    });
    
    var date = new Date();

    setExpiration(date.toLocaleDateString("en-CA"))
  }

  async function  loadCardStudent() {
    setName(student.name);
    setId(student.id);
    setCourse(student.course);
    setRegistrationNumber(student.course);
    setImage({
      imageFile: null,
      imagePhoto: student.photo
    });
    
    var date = new Date(student.validate);

    setExpiration(date.toLocaleDateString("en-CA"))
  }

  async function insertStudent(e: React.FormEvent){
    e.preventDefault();
    try{
    const formData = new FormData();
    formData.append('name', name);
    formData.append('course', course);
    formData.append('validate', expiration);
    formData.append('registrationNumber', registrationNumber);
    formData.append('photo', image.imagePhoto);
    formData.append('imageFile', image.imageFile || defaultPhoto);
    
    await createStudent(formData, authorization)
    toast.success("Estudante cadastrado");
    }catch(err){
      toast.error("Erro ao inserir o estudante");
    }
  }


  return (
    <>
      <div className="create-card">
        <form onSubmit={insertStudent} className="form-create">
          <div className="field-photo">
            <div className="field-photo-image">
                <img className="photo" src={id !== 0 ? `https://localhost:5001/resources/images/${image.imagePhoto}`: image.imagePhoto}  alt="" />
             </div>
            <input type="file" onChange={showPreview} accept="image/*"/>
          </div>
          <div className="field-name">
            <label>Nome: </label>
            <input type="text" value={name} onChange={e => setName(e.target.value)}/>
          </div>
          <div className="field-course">
            <label>Curso: </label>
            <input type="text" value={course} onChange={e => setCourse(e.target.value)}/>
          </div>

          <div className="field-infos">
            <div className="field-code">
              <label>Código do Sistema: </label>
              <input type="text" value={registrationNumber} onChange={(e) => {setRegistrationNumber(e.target.value)}}/>
            </div>
            <div className="field-expirate">
              <label>Validade: </label>
              <input type="date" value={expiration} onChange={e => setExpiration(e.target.value)}/>
            </div>
          </div>
          <button type="submit" className="button-save-continue">Cadastrar Estudante</button>
          {/* <button className="button-create-card-library">Gerar Carteirinha</button>
          <button className="button-clear">Limpar</button> */}
        </form>
        
      </div>
    </>
  );
}
