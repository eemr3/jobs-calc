import LoginForm from '../components/Forms/LoginForm';

export default function Login() {
  return (
    <div className="bg-gray-700 w-full h-screen flex justify-center items-center">
      <div
        className="container flex w-[70%] h-[70%] flex-col bg-gray-700 rounded-lg
            justify-center m-auto  px-6 py-12 lg:px-8"
      >
        <div className="sm:mx-auto sm:w-full sm:max-w-sm">
          <h1 className="font-bold text-[56px] text-center">
            <span className="text-[#FCFDFE]">Jobs</span>
            <span className="text-orange-400">Calc</span>
          </h1>
          <h2 className="mt-10 text-center text-2xl/9 font-bold tracking-tight text-[#FEFCFD]">
            Entre com sua conta
          </h2>
        </div>
        <div>
          <LoginForm />
        </div>
      </div>
    </div>
  );
}
