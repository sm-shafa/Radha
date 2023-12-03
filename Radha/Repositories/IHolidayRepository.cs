﻿using Backend_Assessment.Models;

namespace Radha.Repositories;

public interface IHolidayRepository
{
    public Task<Holiday> GetHoliday(int id);
}