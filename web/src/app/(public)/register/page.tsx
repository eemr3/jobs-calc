import { RegisterForm } from '../../../components/Forms/RegisterForm';

export default function Register() {
  return (
    <>
      <div className="sm:mx-auto sm:w-full sm:max-w-sm">
        <h1 className="font-bold text-[56px] text-center">
          <span className="text-[#FCFDFE]">Jobs</span>
          <span className="text-orange-400">Calc</span>
        </h1>
        <h2 className="mt-10 text-center text-2xl/9 font-bold tracking-tight text-[#FEFCFD]">
          Criar sua conta
        </h2>
      </div>
      <RegisterForm />
    </>
  );
}
