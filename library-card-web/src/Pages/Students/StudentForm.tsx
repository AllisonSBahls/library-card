import React, { useState } from "react";
import { toast } from 'react-toastify'
import { createStudent } from "../../Services/Students";



export default function StudentForm() {
  const defaultPhoto = '/Image/default_photo.png'

  const ImageProps = {
    imageFile: null,
    imagePhoto: defaultPhoto
  }

  const [image, setImage] = useState(ImageProps);
  const [name, setName] = useState('');
  const [course, setCourse]= useState('');
  const [expiration, setExpiration]= useState('');
  const [registrationNumber, setRegistrationNumber] = useState('');

  const token = localStorage.getItem("Token");

  const authorization = {
    headers: {
      Authorization: `Bearer token`,
    },
  };

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
                <img className="photo" src={image.imagePhoto}  alt="" />
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
              <input type="text" value={registrationNumber} onChange={e => setRegistrationNumber(e.target.value)}/>
            </div>
            <div className="field-expirate">
              <label>Validade: </label>
              <input type="Date" value={expiration} onChange={e => setExpiration(e.target.value)}/>
            </div>
          </div>
          <button type="submit" className="button-save-continue">Gerar Carteirinha</button>
          {/* <button className="button-create-card-library">Gerar Carteirinha</button>
          <button className="button-clear">Limpar</button> */},
        </form>
        
      </div>
    </>
  );
}