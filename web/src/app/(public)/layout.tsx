export default function PublicLayout({ children }: { children: React.ReactNode }) {
  return (
    <div className="bg-gray-700 w-full h-screen flex justify-center items-center">
      <div
        className="container flex w-[70%] h-[70%] flex-col bg-gray-700 rounded-lg
          justify-center m-auto  px-6 py-12 lg:px-8"
      >
        {children}
      </div>
    </div>
  );
}
