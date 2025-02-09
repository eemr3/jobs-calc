'use client';
import React, { useState } from 'react';
import { Header } from '../Header';
import Image from 'next/image';
import { calculateFreeHours, countProjects } from '../../Utils/calculate';
import { Avatar } from '../Avatar';
import Link from 'next/link';
import { deleteJob } from '../../service/api/requests';
import toast from 'react-hot-toast';
import { Cards } from './Cards';
import { Icons } from '../Icons';

type JobMainProps = {
  jobsData: Job[];
  planningData: Planning;
  user: User;
};

type Job = {
  jobId?: string;
  name: string;
  dailyHours: number;
  totalHours?: number;
  remainingDays: number;
  valueJob: number;
  status: boolean;
  userId?: number;
};

type User = {
  userId: number;
  fullName: string;
  email: string;
  avatarUrl: string;
};
type Planning = {
  planningId: string;
  monthlyBudget: number;
  daysPerWeek: number;
  hoursPerDay: number;
  vacationPerYear: number;
  valueHour: number;
};

export function JobMain({ jobsData, planningData, user }: JobMainProps) {
  const [jobs, setJobs] = useState<Job[]>(jobsData);

  const handleDelete = async (jobId: string) => {
    try {
      await deleteJob(jobId);
      setJobs((prevJobs) => prevJobs.filter((job) => job.jobId !== jobId));
      return toast.success('Job excluído com sucesso!');
    } catch (error) {
      return toast.error('Houve um erro ao tentar apagar o projeto.');
    }
  };

  return (
    <div className="bg-gray-100 min-h-screen">
      <Header.Root className="page-header bg-gray-700 text-white p-8">
        <Header.Content className="max-w-4xl mx-auto">
          <Header.NavBar className="flex justify-between items-center pb-8">
            <Header.Logo width={208} height={48} />
            <span className="flex gap-2">
              <Image
                src="/images/alert-octagon.svg"
                alt="Alerta"
                width={20}
                height={20}
              />
              {`você tem ${
                jobs.length > 0 ? calculateFreeHours(jobs, planningData) : 0
              } horas livres no seu dia.`}
            </span>
            <Avatar user={user} />
          </Header.NavBar>
          <Header.Separator className="h-[1px] my-4 bg-[#4F4F5B]" />
          <Header.Summary className="mt-8 flex items-center justify-between">
            <div className="info flex gap-8 py-8">
              <Header.Info
                className="text-xl text-[#FCFDFF] grid"
                value={jobs.length > 0 ? countProjects(jobs).totalTjobs : 0}
                text="Projetos ao total"
              />
              <Header.Info
                className="text-xl text-[#FCFDFF] grid"
                value={jobs.length > 0 ? countProjects(jobs).jobsDone : 0}
                text="Em andamento"
              />
              <Header.Info
                className="text-xl text-[#FCFDFF] grid"
                value={jobs.length > 0 ? countProjects(jobs).jobsfinish : 0}
                text="Encerrados"
              />
            </div>
            {planningData.planningId !== '00000000-0000-0000-0000-000000000000' ? (
              <Link
                href="/project/new"
                className="uppercase flex gap-4 bg-orange-400 h-fit px-3 py-2 
                rounded items-center hover:brightness-110 transition-al text-[#FCFDFF]"
              >
                <span className="bg-opacity-10 bg-white rounded p-0.5">
                  <Icons.Plus24 />
                </span>
                <p className="px-2 text-xs font-bold">Adicionar novo job</p>
              </Link>
            ) : (
              <Link
                href="/profile"
                className="uppercase flex gap-4 bg-orange-400 h-fit px-3 py-2 
              rounded items-center hover:brightness-110 transition-al text-[#FCFDFF]"
              >
                {' '}
                <span className="bg-opacity-10 bg-white rounded p-0.5">
                  <svg
                    xmlns="http://www.w3.org/2000/svg"
                    fill="none"
                    viewBox="0 0 24 24"
                    strokeWidth={1.5}
                    stroke="currentColor"
                    className="size-6"
                  >
                    <path
                      strokeLinecap="round"
                      strokeLinejoin="round"
                      d="m16.862 4.487 1.687-1.688a1.875 1.875 0 1 1 2.652 2.652L10.582 16.07a4.5 4.5 0 0 1-1.897 1.13L6 18l.8-2.685a4.5 4.5 0 0 1 1.13-1.897l8.932-8.931Zm0 0L19.5 7.125M18 14v4.75A2.25 2.25 0 0 1 15.75 21H5.25A2.25 2.25 0 0 1 3 18.75V8.25A2.25 2.25 0 0 1 5.25 6H10"
                    />
                  </svg>
                </span>
                <p className="px-2 text-xs font-bold">Criar seu planejamento</p>
              </Link>
            )}
          </Header.Summary>
        </Header.Content>
      </Header.Root>
      <div className="max-w-4xl mx-auto -mt-14">
        <main className="">
          <h1 className="sr-only">Trabalhos</h1>
          <div className="py-6 space-y-2">
            {jobs.length > 0
              ? jobs.map((job) => (
                  <Cards
                    key={job.jobId}
                    jobId={job.jobId}
                    name={job.name}
                    remainingDays={job.remainingDays}
                    status={job.status}
                    valueJob={job.valueJob}
                    onDelete={handleDelete}
                  />
                ))
              : null}
          </div>
        </main>
      </div>
    </div>
  );
}
