import "./style.css";

export default function Students() {
    return (
        <>
            <div className="students-container">

                {/* Cards */}
                <div className="students-content">
                    <div className="students-photo"></div>
                    <div className="students-input">
                        <label>Nome: </label>
                        <input className="students-name" />
                        <label className="students-title-course">Curso: </label>
                        <input className="students-course" />
                    </div>
                    <div className="students-input-info"> 

                        <input className="students-registration" />
                        <label>Código do Sistema: </label>
                        <input className="students-validate" />
                    </div>
                </div>

                <div className="students-content">
                    <div className="students-photo"></div>
                    <div className="students-input">
                        <label>Nome: </label>
                        <input className="students-name" />
                        <label className="students-title-course">Curso: </label>
                        <input className="students-course" />
                    </div>
                    <div className="students-input-info"> 

                        <input className="students-registration" />
                        <label>Código do Sistema: </label>
                        <input className="students-validate" />
                    </div>
                </div>

                <div className="students-content">
                    <div className="students-photo"></div>
                    <div className="students-input">
                        <label>Nome: </label>
                        <input className="students-name" />
                        <label className="students-title-course">Curso: </label>
                        <input className="students-course" />
                    </div>
                    <div className="students-input-info"> 

                        <input className="students-registration" />
                        <label>Código do Sistema: </label>
                        <input className="students-validate" />
                    </div>
                </div>

                <div className="students-content">
                    <div className="students-photo"></div>
                    <div className="students-input">
                        <label>Nome: </label>
                        <input className="students-name" />
                        <label className="students-title-course">Curso: </label>
                        <input className="students-course" />
                    </div>
                    <div className="students-input-info"> 

                        <input className="students-registration" />
                        <label>Código do Sistema: </label>
                        <input className="students-validate" />
                    </div>
                </div>
            </div>
        </>
    )
}