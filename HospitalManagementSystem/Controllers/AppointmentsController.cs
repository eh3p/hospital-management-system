using Microsoft.AspNetCore.Mvc;
using HospitalManagementSystem.DTOs;
using HospitalManagementSystem.Services;

namespace HospitalManagementSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class AppointmentsController : ControllerBase
    {
        private readonly IAppointmentService _appointmentService;
        private readonly ILogger<AppointmentsController> _logger;

        public AppointmentsController(IAppointmentService appointmentService, ILogger<AppointmentsController> logger)
        {
            _appointmentService = appointmentService;
            _logger = logger;
        }

        /// <summary>
        /// Get all appointments
        /// </summary>
        /// <returns>List of all appointments</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<AppointmentDto>>> GetAppointments()
        {
            try
            {
                var appointments = await _appointmentService.GetAllAppointmentsAsync();
                return Ok(appointments);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting appointments");
                return StatusCode(500, "An error occurred while retrieving appointments");
            }
        }

        /// <summary>
        /// Get an appointment by ID
        /// </summary>
        /// <param name="id">Appointment ID</param>
        /// <returns>Appointment details</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<AppointmentDto>> GetAppointment(int id)
        {
            try
            {
                var appointment = await _appointmentService.GetAppointmentByIdAsync(id);
                if (appointment == null)
                {
                    return NotFound($"Appointment with ID {id} not found");
                }

                return Ok(appointment);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting appointment with ID {AppointmentId}", id);
                return StatusCode(500, "An error occurred while retrieving the appointment");
            }
        }

        /// <summary>
        /// Create a new appointment
        /// </summary>
        /// <param name="createAppointmentDto">Appointment data</param>
        /// <returns>Created appointment</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<AppointmentDto>> CreateAppointment(CreateAppointmentDto createAppointmentDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var appointment = await _appointmentService.CreateAppointmentAsync(createAppointmentDto);
                return CreatedAtAction(nameof(GetAppointment), new { id = appointment.Id }, appointment);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating appointment");
                return StatusCode(500, "An error occurred while creating the appointment");
            }
        }

        /// <summary>
        /// Update an appointment
        /// </summary>
        /// <param name="id">Appointment ID</param>
        /// <param name="updateAppointmentDto">Updated appointment data</param>
        /// <returns>Updated appointment</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<AppointmentDto>> UpdateAppointment(int id, UpdateAppointmentDto updateAppointmentDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var appointment = await _appointmentService.UpdateAppointmentAsync(id, updateAppointmentDto);
                if (appointment == null)
                {
                    return NotFound($"Appointment with ID {id} not found");
                }

                return Ok(appointment);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating appointment with ID {AppointmentId}", id);
                return StatusCode(500, "An error occurred while updating the appointment");
            }
        }

        /// <summary>
        /// Delete an appointment
        /// </summary>
        /// <param name="id">Appointment ID</param>
        /// <returns>No content</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteAppointment(int id)
        {
            try
            {
                var result = await _appointmentService.DeleteAppointmentAsync(id);
                if (!result)
                {
                    return NotFound($"Appointment with ID {id} not found");
                }

                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting appointment with ID {AppointmentId}", id);
                return StatusCode(500, "An error occurred while deleting the appointment");
            }
        }

        /// <summary>
        /// Get appointments by patient ID
        /// </summary>
        /// <param name="patientId">Patient ID</param>
        /// <returns>Appointments for the specified patient</returns>
        [HttpGet("patient/{patientId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<AppointmentDto>>> GetAppointmentsByPatient(int patientId)
        {
            try
            {
                var appointments = await _appointmentService.GetAppointmentsByPatientAsync(patientId);
                return Ok(appointments);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting appointments for patient {PatientId}", patientId);
                return StatusCode(500, "An error occurred while retrieving patient appointments");
            }
        }

        /// <summary>
        /// Get appointments by doctor ID
        /// </summary>
        /// <param name="doctorId">Doctor ID</param>
        /// <returns>Appointments for the specified doctor</returns>
        [HttpGet("doctor/{doctorId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<AppointmentDto>>> GetAppointmentsByDoctor(int doctorId)
        {
            try
            {
                var appointments = await _appointmentService.GetAppointmentsByDoctorAsync(doctorId);
                return Ok(appointments);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting appointments for doctor {DoctorId}", doctorId);
                return StatusCode(500, "An error occurred while retrieving doctor appointments");
            }
        }

        /// <summary>
        /// Search appointments with filters
        /// </summary>
        /// <param name="searchDto">Search criteria</param>
        /// <returns>Filtered appointments</returns>
        [HttpGet("search")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<AppointmentDto>>> SearchAppointments([FromQuery] AppointmentSearchDto searchDto)
        {
            try
            {
                var appointments = await _appointmentService.SearchAppointmentsAsync(searchDto);
                return Ok(appointments);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while searching appointments");
                return StatusCode(500, "An error occurred while searching appointments");
            }
        }

        /// <summary>
        /// Check if a time slot is available for a doctor
        /// </summary>
        /// <param name="doctorId">Doctor ID</param>
        /// <param name="appointmentDate">Appointment date</param>
        /// <param name="appointmentTime">Appointment time</param>
        /// <param name="excludeAppointmentId">Appointment ID to exclude from check</param>
        /// <returns>Availability status</returns>
        [HttpGet("availability")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<bool>> CheckTimeSlotAvailability(
            [FromQuery] int doctorId,
            [FromQuery] DateTime appointmentDate,
            [FromQuery] TimeSpan appointmentTime,
            [FromQuery] int? excludeAppointmentId = null)
        {
            try
            {
                var isAvailable = await _appointmentService.IsTimeSlotAvailableAsync(
                    doctorId, appointmentDate, appointmentTime, excludeAppointmentId);
                return Ok(isAvailable);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while checking time slot availability");
                return StatusCode(500, "An error occurred while checking time slot availability");
            }
        }
    }
} 